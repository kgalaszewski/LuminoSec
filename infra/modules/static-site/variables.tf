variable "domain_name" {
  description = "Domain the site will be served on, e.g. luminosec.com or dev.luminosec.com"
  type        = string
}

variable "environment" {
  description = "Environment name (dev, stg, prod) — used to keep resource names unique across environments"
  type        = string
}

variable "zone_name" {
  description = "Route53 hosted zone to create DNS records in. Defaults to domain_name — override when domain_name is a subdomain (e.g. domain_name = stg.luminosec.com, zone_name = luminosec.com)"
  type        = string
  default     = null
}
