using System;
using Tonga;
using Tonga.Map;

namespace Whyre.Wire
{
    /// <summary>
    /// Requirements to a request.
    /// </summary>
    public sealed class Require : MapEnvelope<string, string>
	{
        /// <summary>
        /// Requirements to a request.
        /// </summary>
        public Require(IMap<string,string> head, params IPair<string, string>[] requirements) : base(
            AsMap._(
                Tonga.Enumerable.AsEnumerable._(() =>
                {
                    foreach(var requirement in requirements)
                    {
                        if(!head.Keys().Contains(requirement.Key()))
                            throw new ArgumentException(
                                $"{requirement.Key()} is required");
                        else if (requirement.Value().Length > 0)
                            if (!requirement.Value().Equals(head[requirement.Key()]))
                                throw new ArgumentException(
                                    $"{requirement.Key()} is restricted to '{requirement.Value()}', "
                                    + $"but is '{head[requirement.Key()]}'");
                    }
                    return head.Pairs();
                })
            )
        )
		{ }
    }
}

