#### Mini Test Kit (quick environment/tools check)
Run the commands below (in the container or on the host, depending on your flow) and ensure there are no errors.
```sh
# Check Nix presence
nix --version

# Flake is structured and apps are visible
nix flake show

# .NET info via Nix dev shell
nix develop -c dotnet --info

# Docker and Compose
docker --version && docker compose version

# DB client (one of these, depending on what you installed)
mysql --version || mariadb --version

# jq
jq -V

# Tests via flake app
nix run .#dtest 
```

#### Alternative: Work on the host with direnv (optional)
```sh
# On host
nix --version
sudo apt update && sudo apt install -y direnv
# In project root, add .envrc with 'use flake'
# Example .envrc contents:
# use flake

direnv allow 
```
After direnv allow, entering the project folder will automatically activate the Nix dev environment.