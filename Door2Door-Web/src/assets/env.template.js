// This file is optimized for having values replaced via envsubst
(function(window) {
  window.env = window.env || {};

  // Environment variables.
  window["env"]["apiUrl"] = "${API_URL}";
})(this);