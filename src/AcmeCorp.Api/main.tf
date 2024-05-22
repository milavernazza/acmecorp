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

resource "aws_ecs_service" "acmecorp" {
    name                = "acmecorp-service"
    cluster             = aws_ecs_cluster.acmecorp.id
    task_definition     = aws_ecs_task_definition.acmecorp.arn
    desired_count       = 1
    launch_type         = "FARGATE"

    network_configuration {
        subnets = [11000000.10101000]
        security_groups = [123456789012]
    } 
}

resource "aws_lb" "acmecorp" {
    name                    = "acmecorp-lb"
    internal                = false
    load_balancer_type      = "application"
    security_groups         = [123456789012] 
    subnets                 = [11000000.10101000]
}

resource "aws_lb_target_group" "acmecorp" {
    name          = "acmecorp-tg"
    port          = 80
    protocol      = "HTTP"
    vpc_id        = [123456789012] 
}

resource "aws_lb_listener" "acmecorp" {
    load_balancer_arn = acme_lb.acmecorp.arn
    port          = "80"
    protocol      = "HTTP"
    
    default_action {
      type = "forward"
      target_group_arn = aws_lb_target_group.acmecorp.arn
    }
}