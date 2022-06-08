.PHONY: run

PWD=$(shell pwd)

run:
	docker run -ti \
		-p 7979:7979 \
		-v $(PWD)/prometheus-json-exporter.yml:/config.yml \
		quay.io/prometheuscommunity/json-exporter \
		--config.file=/config.yml