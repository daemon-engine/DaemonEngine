pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                sh 'echo "Building..."'
                sh 'chmod +x Scripts/TestBuild.sh'
                sh 'Scripts/TestBuild.sh'
                archiveArtifacts artifacts: 'Sandbox/bin/Release/net6.0/*', fingerprint: true
            }
        }
        stage('Done') {
            steps {
                sh 'echo "Done!"'
            }
        }
    }
}