import React from 'react';
import { BrowserRouter as Router, Route, Switch, Routes } from 'react-router-dom';
import BookList from './Components/BookForm';
import BookForm from './Components/BookList';
import PublisherList from './Components/PublisherForm';
import PublisherForm from './Components/PublisherList';
import EditBookForm from './Components/EditBookForm';
import EditPublisherForm from './Components/EditPublisherForm';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/books" element={<BookForm />} />
          <Route path="/books/new" element={< BookList />} />
          <Route path="/books/edit/:id" element={<EditBookForm />} />
          <Route path="/publishers" element={< PublisherForm/>} />
          <Route path="/publishers/new" element={<PublisherList />} />
          <Route path="/publishers/edit/:id" element={<PublisherList />} />
        </Routes>
      </div>
    </Router>
  );
}
export default App;