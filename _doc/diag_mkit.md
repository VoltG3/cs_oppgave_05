###### Show the last 50 API log lines and filter for “listening on” to confirm the bound URL/port
```sh
docker compose logs --tail=50 api | grep -i "listening on"
```

###### Show the last 100 lines of API logs
```sh
docker compose logs --tail=100 api
```

###### Show the process/arguments used to start the container (entry command)
```sh
docker inspect cs_oppgave_05-api-1 --format '{{.Path}} {{join .Args " "}}'
```

###### Quick state summary: status, exit code, and any error message
```sh
docker inspect cs_oppgave_05-api-1 --format '{{.State.Status}} {{.State.ExitCode}} {{.State.Error}}'
```

###### Is the API listening *inside* the container?
```sh
docker compose exec api bash -lc 'curl -sS http://localhost:8080/ || true'
```

###### What Entrypoint is baked into the image?
```sh
docker inspect cs_oppgave_05-api-1 --format '{{json .Config.Entrypoint}}'
```