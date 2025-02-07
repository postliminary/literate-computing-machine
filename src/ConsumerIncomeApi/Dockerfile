ARG RUNTIME=linux-x64
ARG CONFIGURATION=release

# -- Build -- #
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG RUNTIME
ARG CONFIGURATION

WORKDIR /app

COPY ./*.config ./
COPY ./src/ConsumerIncomeApi/*.csproj ./src/ConsumerIncomeApi/

RUN dotnet restore ./src/ConsumerIncomeApi --runtime ${RUNTIME}

COPY ./src/ConsumerIncomeApi/ ./src/ConsumerIncomeApi/

RUN dotnet publish ./src/ConsumerIncomeApi -o /app/build --runtime ${RUNTIME} --no-self-contained --no-restore -c ${CONFIGURATION}

# -- Runtime -- #
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run

WORKDIR /app

COPY --from=build /app/build ./

CMD ["./ConsumerIncomeApi"]
