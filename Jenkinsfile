// pipeline {
//     agent any
//     stages {
//         stage('Checkout') {
//             steps {
//                 git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
//             }
//         }
//         stage('Restore') {
//             steps {
//                 bat 'dotnet restore'
//             }
//         }
//         stage('Build') {
//             steps {
//                 bat 'dotnet build TaskManager.sln -c Release'
//             }
//         }
//         stage('Test') {
//             steps {
//                 bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
//             }
//             post {
//                 always {
//                     mstest testResultsFile: 'TestResults/*.trx'
//                 }
//             }
//         }
//         stage('Publish') {
//             steps {
//                 bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o publish'
//             }
//         }
//         stage('Deploy') {
//             steps {
//                 // Stop the IIS site
//                 bat 'powershell -command "Stop-Website -Name TaskManager"'
//                 // Copy published files to IIS folder
//                 bat 'xcopy publish\\* "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y'
//                 // Start the IIS site
//                 bat 'powershell -command "Start-Website -Name TaskManager"'
//             }
//         }
//     }
// }

// pipeline {
//     agent any
//     stages {
//         stage('Checkout') {
//             steps {
//                 git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
//             }
//         }
//         stage('Restore') {
//             steps {
//                 bat 'dotnet restore'
//             }
//         }
//         stage('Build') {
//             steps {
//                 bat 'dotnet build TaskManager.sln -c Release'
//             }
//         }
//         stage('Test') {
//             steps {
//                 bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
//             }
//             post {
//                 always {
//                     mstest testResultsFile: 'TestResults/*.trx'
//                 }
//             }
//         }
//         stage('Publish') {
//             steps {
//                 bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish'
//             }
//         }
//         stage('Deploy') {
//             steps {
//                 bat 'powershell -command "Stop-Website -Name TaskManager"'
//                 bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
//                 bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
//                 bat 'xcopy "%WORKSPACE%\\publish\\*" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y'
//                 bat 'powershell -command "Start-Website -Name TaskManager"'
//             }
//         }
//     }
// }
pipeline {
    agent any
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
                bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
            }
        }
        stage('Deploy') {
            steps {
                bat 'powershell -command "Stop-Website -Name TaskManager"'
                bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
                bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
                bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
                bat 'powershell -command "Start-Website -Name TaskManager"'
            }
        }
    }
}
