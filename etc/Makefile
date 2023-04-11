.PHONY: run

PWD=$(shell pwd)

run:
	docker run -ti \
		-p 7979:7979 \
		-v $(PWD)/prometheus-json-exporter.yml:/config.yml \
		registry.crazyzone.be/json_exporter:0.4.1b \
		--config.file=/config.yml
