# sqs-lambda
simple lambda function to listen to an sqs queue and forward the message to an endpoint

SQS triggering a lambda does a lot of work for you.  When your lambda completes its process, AWS does magic on it's own end to delete the message itself.  https://docs.aws.amazon.com/lambda/latest/dg/with-sqs.html 

This lambda can compliment your elastic beanstalk app (webhost)... instead of setting up another app as a worker app to listen to the sqs queue, create a controller/endpoint to handle the incoming queued messages.  

1. SQS will trigger this lambda  
2. The lambda will forward the `SQSMessage.Body` payload to that endpoint
3. The lambda will auto acknowledge the message on the SQS to delete it  
4. Your EB App will be process the message as if it polled SQS to get it

You might think SNS topc would be your solution for this... but theres an SNS limitation is you need to configure a public endpoint (exposed to the world).  If you set up your EB to be behind a VPC for only your AWS access, you just need to configure your lambda to live in the same VPC and you achieve this same integration
