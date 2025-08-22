{
  description = "Nix dev shell for movies project (db + api + compose)";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.05";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; };
      in {
        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnet-sdk
            aspnetcore-runtime-8
            mysql80            
            git
            curl
            jq
            docker                  
            make
          ];
          shellHook = ''
            echo "Nix dev shell ready. Try: dotnet --info; docker compose ps; mysql --version; jq -V"
          '';
        };
      });
}
