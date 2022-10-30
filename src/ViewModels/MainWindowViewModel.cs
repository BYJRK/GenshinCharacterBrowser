using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GenshinCharacterBrowser.Helpers;
using GenshinCharacterBrowser.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GenshinCharacterBrowser.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Character> charList = new();

    [ObservableProperty]
    Character selectedItem;

    [ObservableProperty]
    BitmapImage backgroundImage;

    [RelayCommand]
    async Task LoadCity(string id)
    {
        var result = await HttpHelper.GetStringAsync($"https://content-static.mihoyo.com/content/ysCn/getContentList?pageSize=20&pageNum=1&order=asc&channelId={id}");

        var list = JObject.Parse(result)["data"]["list"];

        CharList.Clear();
        foreach (var item in list)
        {
            CharList.Add(new Character
            {
                Name = item["title"].ToString(),
                IconUrl = item["ext"].First(v => v["arrtName"].ToString() == "角色-ICON")["value"][0]["url"].ToString(),
                ProtraitUrl = item["ext"].First(v => v["arrtName"].ToString() == "角色-PC端主图")["value"][0]["url"].ToString(),
                NameUrl = item["ext"].First(v => v["arrtName"].ToString() == "角色-名字")["value"][0]["url"].ToString(),
                ElementUrl = item["ext"].First(v => v["arrtName"].ToString() == "角色-属性")["value"][0]["url"].ToString(),
                DialogueUrl = item["ext"].First(v => v["arrtName"].ToString() == "角色-台词")["value"][0]["url"].ToString()
            });
        }

        SelectedItem = CharList[0];

        await ChangeBg(id);
    }

    async Task ChangeBg(string id)
    {
        var backgroundUrl = id switch
        {
            "150" => @"https://uploadstatic.mihoyo.com/contentweb/20200211/2020021114220951905.jpg",
            "151" => @"https://uploadstatic.mihoyo.com/contentweb/20200515/2020051511073340128.jpg",
            "324" => @"https://uploadstatic.mihoyo.com/contentweb/20210719/2021071917030766463.jpg",
            "350" => @"https://webstatic.mihoyo.com/upload/contentweb/2022/08/15/04d542b08cdee91e5dabfa0e85b8995e_8653892990016707198.jpg",
            _ => throw new ArgumentException(id)
        };

        var image = await HttpHelper.GetImageAsync(backgroundUrl);

        BackgroundImage = image;
    }
}
