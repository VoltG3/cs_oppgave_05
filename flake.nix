{
  description = "cs_oppgave_05 dev shell";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.11";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
          config.allowUnfree = true;
        };
        lib = pkgs.lib;
      in {
        devShells.default = pkgs.mkShell {
          packages =
            with pkgs; [
              dotnet-sdk_8
              curl
              bashInteractive
              mariadb-client
            ];

          DOTNET_ROOT = "${pkgs.dotnet-sdk_8}";
          DOTNET_CLI_TELEMETRY_OPTOUT = "1";
          DOTNET_MULTILEVEL_LOOKUP = "0";
        };
      });
}
