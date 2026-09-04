resource "aws_ssm_parameter" "site_bucket_name" {
  name  = "/luminosec/dev/site-bucket-name"
  type  = "String"
  value = module.static_site.site_bucket_name
}

resource "aws_ssm_parameter" "cloudfront_distribution_id" {
  name  = "/luminosec/dev/cloudfront-distribution-id"
  type  = "String"
  value = module.static_site.cloudfront_distribution_id
}
