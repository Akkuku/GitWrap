# GitWrap
Simple application for managing git issues in GitHub and GitLab hosting services, for recruitment purposes.  
The project uses minimal dependencies, follows Clean Architecture and uses CQRS,
which may seem like overkill for current state of the project, but increases readability and provides solid base for future development.

## How to use
1. Create access tokens for GitHub and GitLab with proper access and permissions,
and paste their values into `Config/appsettings.json`.
2. To run project in docker container:
    ```sh
    docker compose up -d
    ```
3. Api will be ready to use on `http://localhost:8080` 

## Endpoints
- `POST api/github/repos/{owner}/{repo}/issues` - Create GitHub issue
- `POST api/gitlab/projects/{projectId}/issues` - Create GitLab issue
- `PATCH api/github/repos/{owner}/{repo}/issues/{issueId}` - Edit GitHub issue name or description
- `PATCH api/gitlab/projects/{projectId}/issues/{issueId}` - Edit GitLab issue name or description
- `PATCH api/github/repos/{owner}/{repo}/issues/{issueId}/close` - Close GitHub issue
- `PATCH api/gitlab/projects/{projectId}/issues/{issueId}/close` - Close GitLab issue

Substitute values with:
- `{projectId}` - The global ID or URL-encoded path of the GitLab project
- `{owner}` - The account owner of the GitHub repository. The name is not case sensitive.
- `{repo}` - The name of the GitHub repository without the .git extension. The name is not case sensitive.
- `{issueId}` - The internal id or number that identifies the issue within the project or repository.