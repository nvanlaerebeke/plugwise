# Plugwise

## Mac Addresses


| Type        | MAC              |
| ----------- | ---------------- |
| Circle Plus | 000D6F0001A5A3B6 |
| Circle      | 000D6F00004B9EA7 |
| Circle      | 000D6F00004B992C |
| Circle      | 000D6F00004BC20A |
| Circle      | 000D6F00004BF588 |
| Circle      | 000D6F00004BA1C6 |
| Circle      | 000D6F000076B9B3 |
| Circle      | 000D6F0000D31AB8 |

## Run using docker

Example command:

```sh
docker run -ti -p 80:80 --device=/dev/ttyUSB0 -e PLUGWISE_SERIAL_PORT=/dev/ttyUSB0 registry.crazyzone.be/plugwise
```

The web API is exposed on port 80, change this if needed.
To pass a USB device, use the `--device` command line options.

Make sure to also pass the correct path to the serial device in the `PLUGWISE_SERIAL_PORT` environment variable.

## Run using kubernetes

Example deployment:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  labels:
    app: plugwise-api
  name:  plugwise-api
spec:
  selector:
    matchLabels:
      app:  plugwise-api
  template:
    metadata:
      labels:
        app:  plugwise-api
    spec:
      containers:
      - image: registry.crazyzone.be/plugwise
        name:  plugwise-api
        volumeMounts:
        - mountPath: /dev/ttyUSB
          name: dev-usb0
        securityContext:
          privileged: true
        ports:
        - containerPort: 80
          name: plugwise-api
        env:
          - name: DEMO_GREETINPLUGWISE_SERIAL_PORTG
            value: "/dev/ttyUSB0"          
      volumes:
      - name: dev-usb0
        hostPath:
          path: /dev/ttyUSB0
```

Make sure to pass the correct USB device and set the environment variable where to find it.
