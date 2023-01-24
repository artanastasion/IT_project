using System;
using System.Text;
using Microsoft.IdentityModel.Tokens; 
namespace IT_project.Authorization;

public class AuthSetting
{
    public const string ISSUER = "IT_Project";
    public const string AUDIENCE = "http://localhost:5000/";
    const string KEY = "key";
    public const int LIFETIME = 1;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}