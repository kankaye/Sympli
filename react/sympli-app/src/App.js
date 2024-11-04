// src/App.js
import axios from 'axios';
import React, { useState } from 'react';
import Spinner from './components/spinner';
import { Button } from './components/button.styled';
import { Container } from './components/container.styled';
import { Form, Input, Label, Title } from './components/form.styled';
import { ResponseContainer, ResponseContent, ResponseTitle } from './components/response.styled';

import styled from 'styled-components';

const ToggleButton = styled.button`
  position: absolute;
  top: 20px;
  right: 20px;
  background: ${({ theme }) => theme.buttonBackground};
  border: none;
  color: #fff;
  padding: 8px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  transition: background-color 0.3s;

  &:hover {
    background-color: ${({ theme }) => theme.buttonHoverBackground};
  }
`;

function App({ toggleEngine, currentEngine }) {
  const [keywords, setKeywords] = useState('');
  const [url, setUrl] = useState('');
  const [response, setResponse] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!keywords.trim() || !url.trim()) {
      alert('Please enter both keywords and URL.');
      return;
    }

    setLoading(true);
    setError(null);
    setResponse(null);

    try {
      const apiUrl = `https://localhost:7041/${currentEngine}/UrlLookup`;

      const res = await axios.get(apiUrl, {
        params: {
          keywords: keywords,
          url: url,
        },
      });

      setResponse(res.data);
    } catch (err) {
      setError(err.message || 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container>
      <ToggleButton onClick={toggleEngine}>
        Switch to {currentEngine === 'Google' ? 'Bing' : 'Google'} Search
      </ToggleButton>
      <Title>Keyword and URL API Caller</Title>
      <Form onSubmit={handleSubmit}>
        <Label htmlFor="keywords">Keywords:</Label>
        <Input
          type="text"
          id="keywords"
          value={keywords}
          onChange={(e) => setKeywords(e.target.value)}
          placeholder="Enter keywords"
        />

        <Label htmlFor="url">URL:</Label>
        <Input
          type="url"
          id="url"
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          placeholder="Enter url. E.g: https://www.sympli.com.au"
          required
        />

        <Button type="submit" disabled={loading}>
          {loading ? (
            <>
              Loading
              <Spinner />
            </>
          ) : (
            'Submit'
          )}
        </Button>
      </Form>

      {error && (
        <ResponseContainer>
          <ResponseTitle>Error</ResponseTitle>
          <ResponseContent>{error}</ResponseContent>
        </ResponseContainer>
      )}

      {response && (
        <ResponseContainer>
          <ResponseTitle>API Response</ResponseTitle>
          <ResponseContent>{JSON.stringify(response, null, 2)}</ResponseContent>
        </ResponseContainer>
      )}
    </Container>
  );
}

export default App;
