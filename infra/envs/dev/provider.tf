terraform {
  required_version = ">= 1.15"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

# eu-central-1 (Frankfurt) chosen for lowest latency from Poland
# (~20ms), not lowest cost — eu-west-1 (Ireland) runs ~7% cheaper
# on S3 and similar on most other services. At this project's
# scale (portfolio, free-tier usage) the cost difference is cents;
# revisit if/when NAT Gateway (VPC phase) becomes the dominant
# cost driver, since that cost is roughly region-independent anyway.
provider "aws" {
  region  = "eu-central-1"
  profile = "luminosec-terraform"
}

# CloudFront requires ACM certificates to be requested in us-east-1,
# regardless of where the rest of the stack lives.
provider "aws" {
  alias   = "us_east_1"
  region  = "us-east-1"
  profile = "luminosec-terraform"
}
