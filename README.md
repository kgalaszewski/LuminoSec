# LuminoSec

Cloud-native security education & consulting platform — AI-assisted
architecture reviews and incident triage, built to demonstrate
production-grade cloud security engineering practices, not just an
LLM wrapper.

Status: early infrastructure stage (Terraform for static site hosting
in progress). Full plan and architecture decisions live in the
private context docs; this README will grow alongside the app.

## Stack

- Backend: .NET 9, modular monolith
- Frontend: Angular 19+ (standalone components, Signals + RxJS)
- AI: Microsoft Agent Framework 1.0 + RAG, **AWS Bedrock** as the LLM provider
- Data: PostgreSQL RDS + pgvector
- Infra: AWS (S3, CloudFront, ECS Fargate, SQS, Cognito, Route 53),
  provisioned with Terraform
- CI/CD: GitHub Actions (Checkov IaC scanning, Trivy, tests)

## Design Notes

- **AWS Bedrock via cross-region inference.** Anthropic Claude models
  have no direct in-region endpoint in eu-central-1 (Frankfurt) — the
  app uses Bedrock's cross-region inference profiles (EU geographic,
  falling back to Global) to reach them. This is AWS's standard,
  fully-supported mechanism, not a workaround: billing stays at
  eu-central-1 (source-region) rates regardless of where the request
  is actually served, so there's no cost penalty. It does mean the
  "traffic never leaves the region" story only holds at the
  network-path level (still private via VPC endpoints, never public
  internet) — not at the "processed exclusively in Frankfurt" level.
  Titan Embeddings, used for the RAG pipeline, are available directly
  in-region.

## Roadmap / Future Work

- **Multi-tenant agent orchestration on EKS/Kubernetes.** Considered
  and deliberately deferred (2026-08-25): running the current
  modular-monolith workload on EKS would add ~$73/mo just for the
  control plane (plus nodes/NAT/networking), disproportionate to the
  app's actual orchestration needs today. ECS Fargate covers the
  container/security story for now. Revisiting this makes sense if
  the platform grows into genuinely multi-tenant, independently
  scaled agent workloads — reflecting where the industry is heading
  with agentic AI on Kubernetes.
