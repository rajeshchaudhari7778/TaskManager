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
                bat 'dotnet build TaskManager.sln -c Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
            }
            post {
                always {
                    mstest testResultsFile: 'TestResults/*.trx'
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
