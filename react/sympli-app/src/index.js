import React, { useEffect, useState } from 'react';
import ReactDOM from 'react-dom/client';
import { ThemeProvider } from 'styled-components';
import App from './App';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { bingTheme, googleTheme } from './themes';

const Root = () => {
  const [searchEngine, setsearchEngine] = useState('Google'); // default theme

  // Load saved theme from localStorage on initial render
  useEffect(() => {
    const savedTheme = localStorage.getItem('search-engine');
    if (savedTheme && (savedTheme === 'Google' || savedTheme === 'Bing')) {
      setsearchEngine(savedTheme);
    }
  }, []);

  const toggleEngine = () => {
    setsearchEngine((prev) => {
      const newEngine = prev === 'Google' ? 'Bing' : 'Google';
      localStorage.setItem('search-engine', newEngine);
      return newEngine;
    });
  };

  return (
    <ThemeProvider theme={searchEngine === 'Google' ? googleTheme : bingTheme}>
      <App toggleEngine={toggleEngine} currentEngine={searchEngine} />
    </ThemeProvider>
  );
};

const container = document.getElementById('root');
const root = ReactDOM.createRoot(container);
root.render(<Root />);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
