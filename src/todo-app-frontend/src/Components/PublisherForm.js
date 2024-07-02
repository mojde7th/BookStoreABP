

import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createPublisher, getPublisher, updatePublisher } from '../services/publisherService';
import { getBooks } from '../services/bookService';

const PublisherForm = () => {
  const [publisher, setPublisher] = useState({ name: '', bookIds: [] });
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { id } = useParams();
  const isEdit = !!id;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const booksResponse = await getBooks();
        const booksData = booksResponse.data.items;
        setBooks(booksData);
        console.log('Books data:', booksData);

        if (isEdit) {
          const publisherResponse = await getPublisher(id);
          const publisherData = publisherResponse.data;
          setPublisher({
            ...publisherData,
            bookIds: publisherData.bookIds || [],
          });
          console.log('Publisher data:', publisherData);

          const updatedBooks = booksData.map(book => ({
            ...book,
            isChecked: publisherData.bookIds.includes(book.id),
          }));
          setBooks(updatedBooks);
        } else {
          const updatedBooks = booksData.map(book => ({
            ...book,
            isChecked: false,
          }));
          setBooks(updatedBooks);
        }

        setLoading(false);
      } catch (error) {
        console.error('Error loading data:', error);
        setLoading(false);
      }
    };

    fetchData();
  }, [id, isEdit]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPublisher((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleBookChange = (e) => {
    const { value, checked } = e.target;
    setPublisher((prevState) => {
      const newBookIds = checked
        ? [...(prevState.bookIds || []), value]
        : (prevState.bookIds || []).filter((bookId) => bookId !== value);
      console.log('Updated bookIds:', newBookIds);
      return { ...prevState, bookIds: newBookIds };
    });

    setBooks((prevBooks) =>
      prevBooks.map((book) =>
        book.id === value ? { ...book, isChecked: checked } : book
      )
    );
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log('Submitting publisher:', publisher);
    try {
      if (isEdit) {
        await updatePublisher(id, publisher);
      } else {
        await createPublisher(publisher);
      }
      navigate('/publishers');
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={publisher.name} onChange={handleChange} required />
      </div>
      <div>
      
        <div>
        <h1>Books</h1>
          {books.length > 0 ? (
            books.map((book) => (
              <div key={book.id}>
                <label>
               
                  <input
                    type="checkbox"
                    value={book.id}
                    checked={book.isChecked}
                    onChange={handleBookChange}
                  />
                  {book.name}
                </label>
              </div>
            ))
          ) : (
            // Displaying disabled checkboxes when no books are available
            <>
              <div>
                <label>
                  <input type="checkbox" disabled /> No books available
                </label>
              </div>
        
            </>
          )}
        </div>
      </div>
      <button type="submit">{isEdit ? 'Update' : 'Create'}</button>
    </form>
  );
};

export default PublisherForm;




