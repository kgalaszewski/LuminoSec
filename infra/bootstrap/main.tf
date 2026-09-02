terraform {
  required_version = ">= 1.15"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }

  backend "s3" {
    bucket       = "luminosec-terraform-state-033855898717-eu-central-1-an"
    key          = "bootstrap/terraform.tfstate"
    region       = "eu-central-1"
    use_lockfile = true
    encrypt      = true
  }
}

provider "aws" {
  region = var.aws_region
}

data "aws_caller_identity" "current" {}

# This bucket is created and owned entirely outside Terraform (manually,
# see SETUP-INSTRUKCJE.txt) — never a `resource`, never imported. That
# keeps this stack from ever managing the very bucket that holds its
# own state. Only the bucket's security configuration (versioning/
# encryption/public access block below) is managed here, via a data
# source lookup of the manually-created bucket.
#
# Created in S3's account regional namespace (not global) — chosen
# deliberately to avoid bucket-squatting.
data "aws_s3_bucket" "tf_state" {
  bucket = "luminosec-terraform-state-${data.aws_caller_identity.current.account_id}-${var.aws_region}-an"
}

resource "aws_s3_bucket_versioning" "tf_state" {
  bucket = data.aws_s3_bucket.tf_state.id
  versioning_configuration {
    status = "Enabled"
  }
}

resource "aws_s3_bucket_server_side_encryption_configuration" "tf_state" {
  bucket = data.aws_s3_bucket.tf_state.id
  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}

resource "aws_s3_bucket_public_access_block" "tf_state" {
  bucket = data.aws_s3_bucket.tf_state.id

  block_public_acls       = true
  block_public_policy     = true
  ignore_public_acls      = true
  restrict_public_buckets = true
}

# SSE above covers encryption at rest; this covers encryption in
# transit — denies any request to the bucket that isn't over TLS.
data "aws_iam_policy_document" "tf_state_enforce_tls" {
  statement {
    sid       = "DenyInsecureTransport"
    effect    = "Deny"
    actions   = ["s3:*"]
    resources = [data.aws_s3_bucket.tf_state.arn, "${data.aws_s3_bucket.tf_state.arn}/*"]

    principals {
      type        = "*"
      identifiers = ["*"]
    }

    condition {
      test     = "Bool"
      variable = "aws:SecureTransport"
      values   = ["false"]
    }
  }
}

resource "aws_s3_bucket_policy" "tf_state_enforce_tls" {
  bucket = data.aws_s3_bucket.tf_state.id
  policy = data.aws_iam_policy_document.tf_state_enforce_tls.json
}
