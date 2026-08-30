# stg (empty)

Not set up yet. When needed: copy the structure of `envs/dev`
(`provider.tf`, `backend.tf`, `main.tf`, `outputs.tf`), point
`backend.tf`'s state `key` at `envs/stg/terraform.tfstate`, and
pick a domain_name that doesn't collide with prod
(e.g. stg.luminosec.com, with zone_name = luminosec.com).
