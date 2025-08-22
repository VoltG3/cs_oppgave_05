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
          config = { allowUnfree = true; };
        };
      in {
        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnet-sdk_8
            aspnetcore-runtime_8
            mysql80
            curl
            bashInteractive
          ];

          DOTNET_ROOT = "${pkgs.dotnet-sdk_8}";
          DOTNET_CLI_TELEMETRY_OPTOUT = "1";
        };
      });
}
