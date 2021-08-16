FROM public.ecr.aws/lambda/dotnet:core3.1

# Copy function code
# COPY publish/* ${LAMBDA_TASK_ROOT}
COPY AWSLambda1/bin/Debug/netcoreapp3.1/linux-x64/publish/* ${LAMBDA_TASK_ROOT}

# RUN apt-get update
# RUN apt-get isntall libc6 libcurl3

# Set the CMD to your handler (could also be done as a parameter override outside of the Dockerfile)
CMD [ "AWSLambda1::AWSLambda1.Function::FunctionHandler" ]
