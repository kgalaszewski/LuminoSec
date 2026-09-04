output "cloudfront_domain_name" {
  value = module.static_site.cloudfront_domain_name
}

output "site_url" {
  value = module.static_site.site_url
}

output "site_bucket_name" {
  value = module.static_site.site_bucket_name
}

output "cloudfront_distribution_id" {
  value = module.static_site.cloudfront_distribution_id
}
