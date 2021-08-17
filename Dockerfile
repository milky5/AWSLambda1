FROM scratch

COPY /work/bash/bash /bin/bash
COPY /work/bash/libtinfo.so.6 /lib64/libtinfo.so.6
COPY /work/bash/libdl.so.2 /lib64/libdl.so.2
COPY /work/bash/libc.so.6 /lib64/libc.so.6
COPY /work/bash/ld-linux-x86-64.so.2 /lib64/ld-linux-x86-64.so.2

ADD /work/static-curl-7.78.0.tar.gz /usr/bin/curl

CMD ["/bin/bash"]
