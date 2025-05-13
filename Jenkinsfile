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
//                 bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
//             }
//         }
//         stage('Deploy') {
//             steps {
//                 bat 'powershell -command "Stop-Website -Name TaskManager"'
//                 bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
//                 bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
//                 bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
//                 bat 'powershell -command "Start-Website -Name TaskManager"'
//                 bat 'iisreset'
//             }
//         }
//     }
// }

pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                script {
                    try {
                        echo "Starting Checkout stage"
                        git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
                        echo "Checkout stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Checkout stage: ${e.message}"
                        error "Checkout stage failed: ${e.message}"
                    }
                }
            }
        }
        stage('Restore') {
            steps {
                script {
                    try {
                        echo "Starting Restore stage"
                        bat 'dotnet restore'
                        echo "Restore stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Restore stage: ${e.message}"
                        error "Restore stage failed: ${e.message}"
                    }
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    try {
                        echo "Starting Build stage"
                        bat 'dotnet clean TaskManager.sln -c Release'
                        bat 'dotnet build TaskManager.sln -c Release'
                        echo "Build stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Build stage: ${e.message}"
                        error "Build stage failed: ${e.message}"
                    }
                }
            }
        }
        stage('Test') {
            steps {
                script {
                    try {
                        echo "Starting Test stage"
                        bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
                        echo "Test stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Test stage: ${e.message}"
                        error "Test stage failed: ${e.message}"
                    }
                }
            }
            post {
                always {
                    mstest testResultsFile: 'TestResults/*.trx'
                }
            }
        }
        stage('Publish') {
            steps {
                script {
                    try {
                        echo "Starting Publish stage"
                        bat 'if exist "%WORKSPACE%\\publish" rd /s /q "%WORKSPACE%\\publish"'
                        bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
                        echo "Publish stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Publish stage: ${e.message}"
                        error "Publish stage failed: ${e.message}"
                    }
                }
            }
        }
        stage('Deploy') {
            steps {
                script {
                    try {
                        echo "Starting Deploy stage"
                        bat 'powershell -command "Stop-Website -Name TaskManager"'
                        bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
                        bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
                        bat 'powershell -command "Start-Sleep -Seconds 5"'
                        bat 'if exist "C:\\inetpub\\wwwroot\\TaskManager" rd /s /q "C:\\inetpub\\wwwroot\\TaskManager"'
                        bat 'mkdir "C:\\inetpub\\wwwroot\\TaskManager"'
                        bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
                        bat 'powershell -command "Start-Website -Name TaskManager"'
                        bat 'iisreset'
                        echo "Deploy stage completed successfully"
                    } catch (Exception e) {
                        echo "Error in Deploy stage: ${e.message}"
                        error "Deploy stage failed: ${e.message}"
                    }
                }
            }
        }
    }
}
