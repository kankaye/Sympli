import styled from 'styled-components';

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 40px;
  background-color: ${({ theme }) => theme.bodyBackground};
  min-height: 100vh;
  transition: background-color 0.3s;
`;