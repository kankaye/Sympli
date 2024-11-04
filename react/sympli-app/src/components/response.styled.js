import styled from 'styled-components';

export const ResponseContainer = styled.div`
  margin-top: 30px;
  width: 80%;
  max-width: 600px;
  background-color: ${({ theme }) => theme.responseBackground};
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s;
`;

export const ResponseTitle = styled.h2`
  margin-bottom: 10px;
  color: ${({ theme }) => theme.textColor};
`;

export const ResponseContent = styled.pre`
  white-space: pre-wrap;
  word-wrap: break-word;
  color: ${({ theme }) => theme.responseText};
`;