#!/bin/bash

set -e
run_cmd="dotnet run"

cd "../Migrations"
until dotnet ef database update -c CoreDbContext; do
>&2 echo "Applying migrations..."
sleep 1
done

>&2 echo "Migrations applied."
cd "../Api"
exec $run_cmd