FROM mono:latest
LABEL maintainer="pichit@sripirom.com"

ADD ./bin/Debug/ .

EXPOSE 5100

CMD ["mono", "BatHawk.NuGetServer.exe"]

