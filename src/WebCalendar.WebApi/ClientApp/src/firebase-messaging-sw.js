﻿importScripts('https://www.gstatic.com/firebasejs/7.14.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/7.14.0/firebase-messaging.js');

const firebaseConfig = {
  apiKey: "AIzaSyCufN3PYtzk5QnwcB_a2Y4udms0EGXbYvw",
  authDomain: "web-calendar-8d135.firebaseapp.com",
  databaseURL: "https://web-calendar-8d135.firebaseio.com",
  projectId: "web-calendar-8d135",
  storageBucket: "web-calendar-8d135.appspot.com",
  messagingSenderId: "699641720827",
  appId: "1:699641720827:web:3aa7f326c85f343e96e55e",
  measurementId: "G-LKZBT7WLNL"
}
firebase.initializeApp(firebaseConfig);

// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();
