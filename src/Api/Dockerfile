FROM microsoft/dotnet:2-sdk
RUN mkdir /app
COPY . /app
WORKDIR /app/Api

RUN dotnet restore
RUN dotnet build

EXPOSE 5000/tcp

WORKDIR /app/Migrations

RUN dotnet restore
RUN dotnet build

WORKDIR /app/Api

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

