output "base_url" {
  value = "${aws_api_gateway_deployment.gateway_deploy.invoke_url}"
}