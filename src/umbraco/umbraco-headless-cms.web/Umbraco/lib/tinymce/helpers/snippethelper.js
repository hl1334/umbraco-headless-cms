function addItems(data, editor) {
  var menu = [];

  for (var i = 0; i < data.length; i++) {
    if (typeof data[i].title === "undefined") {
      continue;
    }

    var menuItem;

    if (typeof data[i].items !== "undefined") {
      menuItem = {
        text: data[i].title,
        menu: addItems(data[i].items, editor)
      };
    } else {
      menuItem = {
        text: data[i].title,
        onclick: onClickHandler(data[i], editor)
      };
    }

    menu.push(menuItem);
  }

  return menu;
}

function onClickHandler(item, editor) {
  return function () {
    if (typeof item.value !== "undefined") {
      editor.insertContent(item.value);
    }
    if (typeof item.onSelect === "function") {
      item.onSelect(item);
    }
  }
}
