/**
 * REMEMBER when creating a new snippets plugin, to whitelist it in the .gitignore
 * under 'Whitelist snippet plugin in TinyMCE'.
 *
 * Umbraco only uses the plugin.min.js file, so whenever editing this file you have to
 * minify it through JSCompress.com or some other tool.
 *
 * This plugin uses some functions which are general for all of our snippet plugins.
 * What this means is that when minifying the file you have to copy-paste the code
 * from '../../helpers/snippethelper.js' to the minify tool just before this script.
 */

tinymce.PluginManager.add('dsSnippets', function (editor) {
  var snippetData = editor.settings.ds_snippetslist;

  if (typeof snippetData === "undefined") {
    throw 'No snippet data provided';
  }

  editor.addButton('dsSnippets', {
    type: 'menubutton',
    tooltip: 'DS snippets',
    image: 'images/editor/snippet.gif',
    menu: addItems(snippetData, editor)
  });
});
