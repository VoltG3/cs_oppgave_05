

#### iac-server
- **What:** CI/CD runner that applies infra from Git (Terraform/Ansible/Pulumi).
- **Does:** pulls PR/merge, runs plan/apply, keeps state (e.g., S3+DynamoDB / Terraform Cloud), stores secrets, supports manual approvals.
- **Why:** versioned, repeatable, auditable infra (not “clicks in console”).
- **Use when:** teams, multiple envs (dev/stage/prod), cloud infra.
- **Skip when:** tiny hobby project on one box; run IaC locally.

### nix-devcontainer
- **What:** dev environment in a container; tooling pinned by Nix.
- **Does:** consistent toolchain (SDK/CLI/DB clients), removes “works on my machine”, fast onboarding (open repo → ready).
- **Why:** reproducible, stable dev across OS/machines.
- **Use when:** teams, polyglot stacks, strict versions.
- **Skip when:** simple solo project with local tool installs.

### Nginx
- **What:** fast web server / reverse proxy.
- **What:** fast web server / reverse proxy.
- **Does:** TLS termination + redirects, route / (SPA) and /api (.NET), compression/cache, load-balancing, limits, security headers.
- **Why:** single domain + HTTPS and a safe front door to services.
- **Use when:** production or multi-app under one domain.
- **Skip when:** local dev with one API on http://localhost:8080

> - IaC server: safe, repeatable infra changes from Git.
> - Nix devcontainer: identical dev env; open repo and code. 
> - Nginx: HTTPS + routing + caching/balancing in front of SPA/API.

#### Simple diagram
```sh
Dev (Nix devcontainer)  ->  Docker Compose (db + api + nginx, local)
CI/CD + IaC server      ->  Cloud (VM/DB/LB via Terraform)
Prod Nginx (TLS/proxy)  ->  Your API/services
```

#### Project skeleton (example)

```sh
project/
  api/                     # .NET API
  docker-compose.yml       # db + api (+ nginx) for dev
  deploy/nginx.conf        # Nginx reverse proxy
  .devcontainer/           # VS Code Dev Container
  flake.nix / shell.nix    # Nix dev env definition
  infra/terraform/         # IaC (VPC, VM, DNS, SSL, etc.)
  .github/workflows/ci.yml # build/test; optional: infra apply
```

#### Recommended rollout
```sh
Db MySql container
Api api container
Nix Nix devcontainer (onboarding/stability)
Nginx Nginx in Compose (dev), later prod with TLS
IaC aC server (stable prod/stage, reproducible infra)
```