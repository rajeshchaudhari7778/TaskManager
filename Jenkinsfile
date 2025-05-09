pipeline {
    agent any
    tools {
        msbuild 'MSBuild'
    }
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
            }
        }
        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                bat 'msbuild TaskManager.sln /p:Configuration=Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:"nunit;LogFilePath=TestResults.xml"'
            }
            post {
                always {
                    nunit testResultsPattern: '**/TestResults.xml'
                }
            }
        }
        stage('Publish') {
            steps {
                bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o publish'
            }
        }
    }
}
