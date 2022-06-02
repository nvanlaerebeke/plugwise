FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./src ./

RUN dotnet restore 
RUN dotnet publish -c Release -o /build -r linux-x64 --self-contained true -p:PublishTrimmed=true /p:DebugSymbols=false /p:DebugType=None "Plugwise/Plugwise.csproj"

# Build runtime image
FROM  mcr.microsoft.com/dotnet/runtime-deps:6.0
WORKDIR /app
EXPOSE 80
ENV PLUGWISE_SERIAL_PORT=/dev/ttyUSB0

COPY --from=build-env /build .
ENTRYPOINT ["/app/Plugwise"]