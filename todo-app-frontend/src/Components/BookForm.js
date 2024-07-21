import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { createBook } from '../services/bookService';
import { getPublishers } from '../services/publisherService';

const BookForm = () => {
  const [book, setBook] = useState({ name: '', author: '', price: 0, publisherId: '' });
  const [publishers, setPublishers] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getPublishers().then(response => setPublishers(response.data));
  }, []);

  const handleChange = e => {
    const { name, value } = e.target;
    setBook(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = e => {
    e.preventDefault();
    createBook(book)
      .then(() => navigate('/books'))
      .catch(error => console.error('Error saving book:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={book.name} onChange={handleChange} required />
      </div>
      <div>
        <label>Author</label>
        <input type="text" name="author" value={book.author} onChange={handleChange} required />
      </div>
      <div>
        <label>Price</label>
        <input type="number" name="price" value={book.price} onChange={handleChange} required />
      </div>
      <div>
        <label>Publisher</label>
        <select name="publisherId" value={book.publisherId} onChange={handleChange} required>
          <option value="">Select Publisher</option>
          {publishers.map(publisher => (
            <option key={publisher.id} value={publisher.id}>{publisher.name}</option>
          ))}
        </select>
      </div>
      <button type="submit">Save</button>
    </form>
  );
};

export default BookForm;
