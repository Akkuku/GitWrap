using System;
using System.Net.Http.Headers;
using GitWrap.Application.Interfaces;
using GitWrap.Infrastructure.Constants;
using GitWrap.Infrastructure.GitHub;
using GitWrap.Infrastructure.GitHub.Issues;
using GitWrap.Infrastructure.GitLab;
using GitWrap.Infrastructure.GitLab.Issues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitWrap.Infrastructure;

public static class Configuration
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .ConfigureHttpClients(configuration)
            .RegisterServices();

    private static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var gitHubConfiguration = configuration.GetRequiredSection("GitHub").Get<GitHubConfiguration>()!;
        var gitLabConfiguration = configuration.GetRequiredSection("GitLab").Get<GitLabConfiguration>()!;

        services.AddHttpClient(
            HttpClientName.GitHub,
            client =>
            {
                client.BaseAddress = new Uri(gitHubConfiguration.BaseAddress);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", gitHubConfiguration.AccessToken);
                client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", gitHubConfiguration.ApiVersion);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github.raw+json");
            });

        services.AddHttpClient(
            HttpClientName.GitLab,
            client =>
            {
                client.BaseAddress = new Uri(gitLabConfiguration.BaseAddress);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", gitLabConfiguration.AccessToken);
            });

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IGitHubClient, GitHubClient>();
        services.AddScoped<IGitLabClient, GitLabClient>();
        services.AddScoped<IGitIssuesRepository, GitLabIssuesRepository>();
        services.AddScoped<IGitIssuesRepository, GitHubIssuesRepository>();

        return services;
    }
}