﻿using LMACO.Interfaces.Services;
using SharpToken;

namespace LMACO.Services;

internal class GptRequestCostSimulator : IGptRequestCostSimulator
{
    public double GetPromptPrice(string prompt)
    {
        var encoding = GptEncoding.GetEncodingForModel("gpt-4");
        var encoded = encoding.Encode(prompt);

        return encoded.Count * (0.03/1000);
    }
}