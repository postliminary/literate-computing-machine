ARG RUNTIME=linux-x64
ARG CONFIGURATION=release

# -- Build -- #
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG RUNTIME
ARG CONFIGURATION

WORKDIR /app

COPY ./*.config ./
COPY ./src/CarrierIncomeApi/*.csproj ./src/CarrierIncomeApi/

RUN dotnet restore ./src/CarrierIncomeApi --runtime ${RUNTIME}

COPY ./src/CarrierIncomeApi/ ./src/CarrierIncomeApi/

RUN dotnet publish ./src/CarrierIncomeApi -o /app/build --runtime ${RUNTIME} --no-self-contained --no-restore -c ${CONFIGURATION}

# -- Runtime -- #
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run

WORKDIR /app

COPY --from=build /app/build ./

CMD ["./CarrierIncomeApi"]
