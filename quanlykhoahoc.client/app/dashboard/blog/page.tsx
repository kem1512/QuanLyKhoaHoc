"use client";

import { BlogClient, BlogMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardBlog() {
  const BlogService = new BlogClient();

  return (
    <DataTable
      url="/blog"
      fields={Object.keys(new BlogMapping().toJSON()).filter(
        (c) => c !== "creator"
      )}
      deleteAction={(id) => BlogService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        BlogService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
