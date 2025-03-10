﻿using System.Text.Json.Serialization;

namespace MovieService.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Genre
{
    Action = 1,
    Drama = 2,
    Comedy = 3,
    Horror = 4,
    Romance = 5,
    Thriller = 6,
    SciFi = 7
}