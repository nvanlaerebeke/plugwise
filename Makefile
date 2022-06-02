.PHONY: build run

PROJECT="Plugwise"
PROJECT_LOWER=$(shell echo $(PROJECT) | tr A-Z a-z)
PWD=$(shell pwd)

REGISTRY:=registry.crazyzone.be
VERSION:=$(shell cat VERSION | tr --delete '/n')
ARCH:=linux-x64
PORT:=8080

clean:
	rm -rf build

build:
	mkdir -p "${PWD}/build/"
	dotnet restore "${PWD}/src/Plugwise" && \
	dotnet publish -c Release -o "${PWD}/build" -r ${ARCH} --self-contained true -p:PublishTrimmed=true /p:DebugSymbols=false /p:DebugType=None "${PWD}/src/Plugwise/Plugwise.csproj"

container:
	docker build -t "${REGISTRY}/${PROJECT_LOWER}:${VERSION}" .

run: container
	docker run -ti --rm \
		--name "${PROJECT_LOWER}" \
		-p 8080:80 \
		${REGISTRY}/${PROJECT_LOWER}:${VERSION}

push: container
	docker push ${REGISTRY}/${PROJECT_LOWER}:${VERSION}
	docker tag ${REGISTRY}/${PROJECT_LOWER}:${VERSION} ${REGISTRY}/${PROJECT_LOWER}:latest
	docker push ${REGISTRY}/${PROJECT_LOWER}:latest

