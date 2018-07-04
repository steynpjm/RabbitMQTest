# escape=`

FROM microsoft/windowsservercore
ENV ERLANG_HOME="c:\erlang"
SHELL [ "powershell", "-command"]
ARG RABBITMQ_VERSION=3.7.6
COPY otp_win32_20.3.exe c:\erlang_install.exe
COPY rabbitmq_server-${RABBITMQ_VERSION} c:\rabbitmq-${RABBITMQ_VERSION}\
RUN Start-Process -Wait -FilePath "c:\\erlang_install.exe" -ArgumentList /S, /D=$env:ERLANG_HOME ; Remove-Item -Force -Path "C:\\erlang_install.exe" ;
ENV RABBITMQ_VERSION=${RABBITMQ_VERSION}
ENV RABBITMQ_SERVER=C:\rabbitmq_server-${RABBITMQ_VERSION}
RUN $path = [Environment]::GetEnvironmentVariable('Path', 'Machine');  [Environment]::SetEnvironmentVariable('Path', $path + ';C:\rabbitmq-' + $env:RABBITMQ_VERSION + '\sbin', 'Machine')
RUN rabbitmq-plugins.bat enable rabbitmq_management --offline
COPY runme.bat c:\rabbitmq-${RABBITMQ_VERSION}\sbin\runme.bat
COPY rabbitmq.conf C:\Users\ContainerAdministrator\AppData\Roaming\RabbitMQ\rabbitmq.conf
ENTRYPOINT rabbitmq-server.bat
EXPOSE 5672 15672