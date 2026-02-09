using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.AI.Agents.common;

public class AgentDecision
{
    public bool Success { get; set; }
    public string Summary { get; set; }
    public Dictionary<string, string> Data { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
}