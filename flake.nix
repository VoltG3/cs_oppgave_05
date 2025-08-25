{
  description = "cs_oppgave_05 – minimal devShell + dtest app";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.11";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; config.allowUnfree = true; };
        dotnet = pkgs.dotnet-sdk_8;

        # dtest clean wrapper without: unpackPhase/src
        dtestDrv = pkgs.runCommand "dtest" { buildInputs = [ pkgs.makeWrapper dotnet ]; } ''
          mkdir -p "$out/bin"
          makeWrapper ${dotnet}/bin/dotnet "$out/bin/dtest" --add-flags "test --nologo"
        '';
      in
      {
        devShells.default = pkgs.mkShell {
          packages = with pkgs; [ dotnet mariadb-client curl bashInteractive ];
        };

        apps = {
          default = { type = "app"; program = "${dtestDrv}/bin/dtest"; };
          dtest   = { type = "app"; program = "${dtestDrv}/bin/dtest"; };
        };
      }
    );
}



