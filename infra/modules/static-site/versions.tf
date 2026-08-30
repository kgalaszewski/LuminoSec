terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
      # ACM certs for CloudFront must be requested in us-east-1,
      # regardless of the region the rest of the stack lives in —
      # the calling env passes that provider in explicitly.
      configuration_aliases = [aws.us_east_1]
    }
  }
}
