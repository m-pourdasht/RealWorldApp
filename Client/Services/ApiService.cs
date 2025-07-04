﻿using System.Net;
using System.Net.Http.Json;
using RealWorldApp.Shared.Models;


public class ApiService<T>
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public ApiService(HttpClient httpClient, string endpoint)
    {
        _httpClient = httpClient;
        _endpoint = endpoint;
    }

    public async Task<ApiResult<List<T>>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_endpoint);
            var data = await response.Content.ReadFromJsonAsync<List<T>>();
            return new ApiResult<List<T>>
            {
                Success = response.IsSuccessStatusCode,
                Data = data,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<List<T>>
            {
                Success = false,
                Data = null,
                StatusCode = 0,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResult<T?>>GetAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_endpoint}/{id}");
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResult<T?>
            {
                Success = true,
                Data = data,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<T?>
            {
                Success = false,
                Data = default,
                StatusCode = 0,
                ErrorMessage = ex.Message
            };
        }
    }


    //public async Task<T?> GetAsync(int id)
    //   => await _httpClient.GetFromJsonAsync<T>($"{_endpoint}/{id}");

    public async Task<ApiResult<T>> CreateAsync(T dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, dto); // Serialization
            var data = await response.Content.ReadFromJsonAsync<T>(); // Deserialization
            return new ApiResult<T>
            {
                Success = response.IsSuccessStatusCode,
                Data = data,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<T>
            {
                Success = false,
                Data = default,
                StatusCode = 0,
                ErrorMessage = ex.Message
            };
        }
    }
    //public async Task<T> CreateAsync(T dto)
    //{
    //    var response = await _httpClient.PostAsJsonAsync(_endpoint, dto);
    //    response.EnsureSuccessStatusCode();
    //    return await response.Content.ReadFromJsonAsync<T>();
    //}

    public async Task<ApiResult<T>> UpdateAsync(int id, T dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", dto); // Serialization
            var data = await response.Content.ReadFromJsonAsync<T>(); // Deserialization
            return new ApiResult<T>
            {
                Success = response.IsSuccessStatusCode,
                Data = data,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<T>
            {
                Success = false,
                Data = default,
                StatusCode = 0,
                ErrorMessage = ex.Message
            };
        }
    }
    //var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", dto);
    //response.EnsureSuccessStatusCode();
    public async Task<ApiResult<T>> DeleteAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResult<T>
            {
                Success = response.IsSuccessStatusCode,
                Data = data,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? null : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResult<T>
            {
                Success = false,
                Data = default,
                StatusCode = 0,
                ErrorMessage = ex.Message
            };
        }
    }
//    var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
//    response.EnsureSuccessStatusCode();
}