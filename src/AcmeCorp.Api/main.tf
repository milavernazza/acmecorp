provider "aws" {
    region = "us-east-1"
}

resource "aws_ecs_cluster" "acmecorp" {
    name = "acmecorp-cluster"
}

resource "aws_ecs_task_definition" "acmecorp" {
    family                        = "acmecorp-task"
    network_mode                  = "awsvpc"
    requires_compatibilities      = ["FARGATE"]
    cpu                           = "256"
    memory                        = "512"
    container_definitions = jsonencode([
        {
            name        = "acmecorp-api"
            image       = "acmecorpapi:latest"
            essential   = true
            portMappings = [
                {
                    containerPort = 80
                    hostPort      = 80
                }
            ]
        }
    ])
}