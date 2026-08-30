terraform {
  required_version = ">= 1.15"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = var.aws_region
}

# CloudFront requires ACM certificates to be requested in us-east-1,
# regardless of where the rest of the stack lives.
provider "aws" {
  alias  = "us_east_1"
  region = "us-east-1"
}
